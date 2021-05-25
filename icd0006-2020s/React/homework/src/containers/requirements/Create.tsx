import { useState, useContext } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { IRequirement } from "../../dto/IRequirement";
import { BaseService } from "../../services/base-service";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const initialRequirementValues: IRequirement = {
    id: '',
    name: '',
    nameId: '',
    description: '',
    descriptionId: '',
    amount: null,
    price: 0,
};

const Create = () => {
    const appState = useContext(AppContext);
    const [requirementData, setRequirementData] = useState(initialRequirementValues);
    const [pass, setPass] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');


    const UpdateClicked = async (e: Event) => {
        console.log("Clocked1");
        e.preventDefault();
        if (requirementData.name === '' || requirementData.description === '' || requirementData.price === null) {
            setAlertMessage('Empty value!');
        } else {
            setAlertMessage('');
            let response = await BaseService.create<IRequirement>('/requirements', { name: requirementData.name, description: requirementData.description, amount: requirementData.amount, price: requirementData.price }, appState.jwt!);
            console.log(response);
            if (!response.ok) {
                setAlertMessage(response.messages![0]);
            } else {
                setAlertMessage('');
                setPass(true);
            }
        }
    }


    return (
        <>
            { pass ? <Redirect to="/requirements" /> : null}
            <h1>Create</h1>
            <form onSubmit={(e) => e.preventDefault()}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />

                            <div className="form-group">
                                <label htmlFor="inputName">Name</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    id="inputName"
                                    name="inputName"
                                    value={requirementData.name} onChange={(e) => setRequirementData({ ...requirementData, name: e.target.value })}
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="inputDescription">Description</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    id="inputDescription"
                                    name="inputDescription"
                                    value={requirementData.description} onChange={(e) => setRequirementData({ ...requirementData, description: e.target.value })}
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="inputPrice">Price</label>
                                <input
                                    className="form-control"
                                    type="number"
                                    id="inputPrice"
                                    name="inputPrice"
                                    value={requirementData.price} onChange={(e) => setRequirementData({ ...requirementData, price: Number(e.target.value) })}
                                />
                            </div>
                            {requirementData.amount !== null ?
                                <div className="form-group">
                                    <label htmlFor="inputAmount">Amount</label>
                                    <input
                                        className="form-control"
                                        type="number"
                                        id="inputAmount"
                                        name="inputAmount"
                                        value={requirementData.amount!} onChange={(e) => setRequirementData({ ...requirementData, amount: Number(e.target.value) })}
                                    />
                                </div>
                                :
                                <></>
                            }
                            <div className="form-group">
                                <button onClick={(e) => UpdateClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
            <Link to={'/requirements'}>Back to list</Link>
        </>
    );

}

export { Create };

import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { IRequirement } from "../../dto/IRequirement";
import Loader from "../../components/Loader";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const initialRequirementValues: IRequirement = {
    id: '',
    name: '',
    nameId: '',
    description: '',
    descriptionId: '',
    amount: 0,
    price: 0,
};

const Edit = () => {
    const appState = useContext(AppContext);
    const { id } = useParams() as IRouteId;
    const [requirementData, setRequirementData] = useState(initialRequirementValues);
    const [pass, setPass] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.get<IRequirement>('/requirements', id, appState.jwt!);

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setRequirementData(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData();// eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const UpdateClicked = async (e: Event) => {
        e.preventDefault();
        if (requirementData.name === '' || requirementData.description === '' || requirementData.price === null) {
            setAlertMessage('Empty value!');
        };
        setAlertMessage('');
        let response = await BaseService.update<IRequirement>('/requirements', id, requirementData, appState.jwt!);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            setPass(true);
        }
    }

    const DeleteClicked = async (e: Event) => {
        e.preventDefault();
        setAlertMessage('');
        let response = await BaseService.delete<IRequirement>('/requirements', id, appState.jwt!);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            setPass(true);
        }

    }

    return (
        <>
            { pass ? <Redirect to="/requirements" /> : null}
            <h1>Edit</h1>
            <form onSubmit={(e) => e.preventDefault()}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <Loader {...pageStatus} />
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
                                <button onClick={(e) => UpdateClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Update</button>
                                <button onClick={(e) => DeleteClicked(e.nativeEvent)} type="submit" className="btn btn-danger float-right">Delete</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
            <Link to={'/Names'}>Back to list</Link>
        </>
    );
}

export { Edit }

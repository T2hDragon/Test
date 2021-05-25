import { useState, useContext } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { ITitle } from "../../dto/ITitle";
import { BaseService } from "../../services/base-service";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const initialTitleValues: ITitle = {
    id: '',
    name: '',
    nameId: ''
};

const Create = () => {
    const appState = useContext(AppContext);
    const [titleData, setTitleData] = useState(initialTitleValues);
    const [pass, setPass] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');


    const UpdateClicked = async (e: Event) => {
        e.preventDefault();
        if (titleData.name === '') {
            setAlertMessage('Empty name!');
        };
        setAlertMessage('');
        let response = await BaseService.create<ITitle>('/titles', { name: titleData.name }, appState.jwt!);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            setPass(true);
        }
    }

    return (
        <>
            { pass ? <Redirect to="/titles" /> : null}
            <h1>Create</h1>
            <form onSubmit={(e) => e.preventDefault()}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />

                            <div className="form-group">
                                <label htmlFor="inputTitleName">Title</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    id="inputTitleName"
                                    name="inputTitleName"
                                    value={titleData.name} onChange={(e) => setTitleData({ ...titleData, name: e.target.value })}
                                />
                            </div>
                            <div className="form-group">
                                <button onClick={(e) => UpdateClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
            <Link to={'/titles'}>Back to list</Link>
        </>
    );
}

export { Create }

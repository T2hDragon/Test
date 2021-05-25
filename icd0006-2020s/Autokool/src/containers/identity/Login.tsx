import React, { useContext, useState } from "react";
import { Redirect } from "react-router";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IdentityService } from "../../services/identity-service";



const Login = () => {
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.identity.login;

    const [loginData, setLoginData] = useState({ userName: '', password: '' });
    const [alertMessage, setAlertMessage] = useState('');

    const logInClicked = async (e: Event) => {
        e.preventDefault();
        if (loginData.userName === '' || loginData.password === '') {
            setAlertMessage(resource.emptyInputs);
        };
        setAlertMessage('');
        let response = await IdentityService.Login(loginData, appState.currentLanguage.name);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            appState.jwt = response.data!.token;
            appState.userName = response.data!.userName;
            appState.firstName = response.data!.firstname;
            appState.lastName = response.data!.lastname;
            appState.title = null;
            appState.contractId = null;
            appState.setAuthInfo(appState.jwt, appState.userName, appState.firstName, appState.lastName, appState.title, appState.contractId, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
        }
    }

    return (
        <>
            { appState.jwt !== null ? <Redirect to="/" /> : null}
            <form onSubmit={(e) => logInClicked(e.nativeEvent)}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />
                            <div className="form-group">
                                <label htmlFor="Input_username">{resource.username}</label>
                                <input value={loginData.userName} onChange={e => setLoginData({ ...loginData, userName: e.target.value })} className="form-control" type="text" id="Input_Username" name="Input.Username" placeholder="" autoComplete="username" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_Password">{resource.password}</label>
                                <input value={loginData.password} onChange={e => setLoginData({ ...loginData, password: e.target.value })} className="form-control" type="password" id="Input_Password" name="Input.Password" placeholder="" autoComplete="current-password" />
                            </div>
                            <div className="form-group">
                                <button onClick={(e) => logInClicked(e.nativeEvent)} type="submit" className="btn btn-primary">{resource.logIn}</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
        </>
    );
}

export default Login;
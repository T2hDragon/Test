import React, { useContext, useState } from "react";
import { Redirect } from "react-router";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IdentityService } from "../../services/identity-service";



const Login = () => {
    const appState = useContext(AppContext);

    const [loginData, setLoginData] = useState({ userName: '', password: '' });
    const [alertMessage, setAlertMessage] = useState('');

    const logInClicked = async (e: Event) => {
        e.preventDefault();
        if (loginData.userName === '' || loginData.password === '') {
            setAlertMessage('Empty username or password!');
        };
        setAlertMessage('');
        let response = await IdentityService.Login('account/login', loginData);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            appState.setAuthInfo(response.data!.token, response.data!.userName, response.data!.firstname, response.data!.lastname);
        }
    }

    return (
        <>
            { appState.jwt !== null ? <Redirect to="/" /> : null}
            <h1>Log in</h1>
            <form onSubmit={(e) => logInClicked(e.nativeEvent)}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />
                            <div className="form-group">
                                <label htmlFor="Input_username">Username</label>
                                <input value={loginData.userName} onChange={e => setLoginData({ ...loginData, userName: e.target.value })} className="form-control" type="text" id="Input_Username" name="Input.Username" placeholder="username" autoComplete="username" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_Password">Password</label>
                                <input value={loginData.password} onChange={e => setLoginData({ ...loginData, password: e.target.value })} className="form-control" type="password" id="Input_Password" name="Input.Password" placeholder="Input your current password..." autoComplete="current-password" />
                            </div>
                            <div className="form-group">
                                <button onClick={(e) => logInClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Log in</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
        </>
    );
}

export default Login;
import { useContext } from "react";
import { AppContext } from "../context/AppContext";
import { NavLink, useHistory as UseHinstory } from "react-router-dom";
import { useLocation as UseLocation } from 'react-router-dom'
import { ISupportedLanguage } from "../dto/ISupportedLanguage";
import { LangService } from '../services/lang-service';

const Header = () => {
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.components.header;
    const history = UseHinstory();
    const location = UseLocation();
    const changeLanguageClicked = async (e: Event, lang: ISupportedLanguage) => {
        if (lang.name === appState.currentLanguage.name) return;
        appState.currentLanguage = lang;
        let response = await LangService.getLangResources(appState.currentLanguage.name);
        if (!response.ok) {
        } else {
            appState.langResources = response.data!;
            appState.setAuthInfo(appState.jwt, appState.userName, appState.firstName, appState.lastName, appState.title, appState.contractId, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
            const currentPage = location.pathname
            history.push("/langRefresh");
            history.push(currentPage);
        }
    }

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar navbar-dark bg-dark border-bottom box-shadow mb-3">
                <div className="container">
                    <NavLink className="navbar-brand" to="/">{resource.brand}</NavLink>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            {appState.title === "teacher" ?
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link" to={"/teacher/" + appState.contractId + "/overview"}>{resource.overview}</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink className="nav-link" to={"/teacher/" + appState.contractId + "/schedule"}>{resource.schedule}</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink className="nav-link" to={"/teacher/" + appState.contractId + "/students"}>{resource.students}</NavLink>
                                    </li>
                                </>
                                : null
                            }
                            {appState.title === "student" ?
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link" to={"/student/courses/" + appState.contractId}>{resource.courses}</NavLink>
                                    </li>
                                </>
                                : null
                            }
                            <li className="nav-item dropdown">
                                <div className="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <img id="nav-translation-image" src={`${process.env.PUBLIC_URL}/assets/images/Translation-Icon-White.png`} alt="translation picker logo"></img>

                                </div>
                                <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                                    {appState.supportedLanguages.map(language =>
                                        <div key={language.name} className="dropdown-item nav-link" onClick={(e) => changeLanguageClicked(e.nativeEvent, language)} >
                                            {language.nativeName}
                                        </div>
                                    )}
                                </div>
                            </li>
                        </ul>
                        <ul className="navbar-nav">
                            {appState.jwt === null ?
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link text-info d-block" to="/identity/login">{resource.logIn}</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink className="nav-link text-info d-block" to="/identity/register">{resource.register}</NavLink>
                                    </li>
                                </>
                                :
                                <>
                                    <li>
                                    </li>
                                    <li className="nav-item text-center">
                                        <span className="username">{appState.userName}</span>
                                        <button onClick={() => {
                                            appState.setAuthInfo(null, '', '', '', null, null, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
                                            history.push("/");
                                        }
                                        } className="btn btn-link nav-link text-info" >{resource.logOut}</button>

                                    </li>
                                </>
                            }
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;
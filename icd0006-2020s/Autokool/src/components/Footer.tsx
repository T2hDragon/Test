import { useContext } from "react";
import { AppContext } from "../context/AppContext";
const Footer = () => {
    const appState = useContext(AppContext);
    return (
        <footer className="border-top footer text-muted">
            <div className="container">
                &copy; 2021 - {appState.langResources.frontEnd.components.footer.appName}
            </div>
        </footer>
    );
}

export default Footer;
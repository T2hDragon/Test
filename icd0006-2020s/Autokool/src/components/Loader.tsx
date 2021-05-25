import { useContext } from "react";
import { AppContext } from "../context/AppContext";
import { EPageStatus } from "../types/EPageStatus";

const Loader = (props: { pageStatus: EPageStatus, statusCode: number }) => {
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.components.loader;

    if (props.pageStatus === EPageStatus.Loading) {
        return <div className="alert alert-primary" role="alert">{resource.loading}...</div>;
    }
    if (props.pageStatus === EPageStatus.Error) {
        return <div className="alert alert-danger" role="alert">{resource.error}... {props.statusCode}</div>
    }
    return <></>;
}

export default Loader;
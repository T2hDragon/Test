import { useContext } from "react";
import { AppContext } from "../context/AppContext";

const Page404 = () => {
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.page404;
    return (
        <h1 className="text-danger">{resource.notFound} - 404!</h1>
    );
}

export default Page404;
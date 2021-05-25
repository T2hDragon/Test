import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import { ITitle } from "../../dto/ITitle";
import Loader from "../../components/Loader";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";

const initialTitleValues: ITitle = {
    id: '',
    name: '',
    nameId: ''
};

const Details = () => {
    const appState = useContext(AppContext);
    const { id } = useParams() as IRouteId;
    const [title, setTitle] = useState(initialTitleValues);


    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.get<ITitle>('/titles', id, appState.jwt!);

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setTitle(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData(); // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Title</h4>
                <hr />
                <Loader {...pageStatus} />

                <dl className="row">
                    <dt className="col-sm-4">Id</dt>
                    <dd className="col-sm-8">
                        {title.id}
                    </dd>

                    <dt className="col-sm-4">Name</dt>
                    <dd className="col-sm-8">
                        {title.name}
                    </dd>
                </dl>
            </div>
            <div>
                <Link
                    to="/titles"
                >Back to list</Link>
            </div >
        </>
    );
}

export { Details };
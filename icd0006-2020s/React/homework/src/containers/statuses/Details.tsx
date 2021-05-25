import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import { IStatus } from "../../dto/IStatus";
import Loader from "../../components/Loader";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";

const initialStatusValues: IStatus = {
    id: '',
    name: '',
    nameId: ''
};

const Details = () => {
    const appState = useContext(AppContext);
    const { id } = useParams() as IRouteId;
    const [status, setStatus] = useState(initialStatusValues);


    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.get<IStatus>('/statuses', id, appState.jwt!);

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStatus(result.data);
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
                <h4>Status</h4>
                <hr />
                <Loader {...pageStatus} />

                <dl className="row">
                    <dt className="col-sm-4">Id</dt>
                    <dd className="col-sm-8">
                        {status.id}
                    </dd>

                    <dt className="col-sm-4">Name</dt>
                    <dd className="col-sm-8">
                        {status.name}
                    </dd>
                </dl>
            </div>
            <div>
                <Link
                    to="/statuses"
                >Back to list</Link>
            </div >
        </>
    );
}

export { Details };
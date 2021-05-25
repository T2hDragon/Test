import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import Loader from "../../components/Loader";
import { IStatus } from "../../dto/IStatus";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";



const RowDisplay = (props: { status: IStatus }) => (
    <tr>
        <td>
            {props.status.name}
        </td>
        <td className="float-right">
            <Link to={'/statuses/' + props.status.id}>Details</Link>
            {useContext(AppContext).jwt === null ?
                <></>
                :
                <>
                    <span> | </span>
                    <Link to={'/statuses/edit/' + props.status.id}>Edit</Link>
                </>
            }
        </td>
    </tr>
);

const Index = () => {
    const [Statuses, setStatuses] = useState([] as IStatus[]);

    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.getAll<IStatus>('/statuses');

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStatuses(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            <h1>Statuses</h1>
            <Link to={'/statuses/create'}>Create new status</Link>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Status
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {Statuses.map(status =>
                        <RowDisplay status={status} key={status.id} />)
                    }
                </tbody>
            </table>
            <Loader {...pageStatus} />
        </>
    );
}

export { Index };
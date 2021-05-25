import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import Loader from "../../components/Loader";
import { ITitle } from "../../dto/ITitle";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";



const RowDisplay = (props: { title: ITitle }) => (
    <tr>
        <td>
            {props.title.name}
        </td>
        <td className="float-right">
            <Link to={'/titles/' + props.title.id}>Details</Link>
            {useContext(AppContext).jwt === null ?
                <></>
                :
                <>
                    <span> | </span>
                    <Link to={'/titles/edit/' + props.title.id}>Edit</Link>
                </>
            }
        </td>
    </tr>
);

const Index = () => {
    const [titles, setTitles] = useState([] as ITitle[]);

    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.getAll<ITitle>('/titles');

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setTitles(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            <h1>Titles</h1>
            <Link to={'/titles/create'}>Create new title</Link>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Title
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {titles.map(title =>
                        <RowDisplay title={title} key={title.id} />)
                    }
                </tbody>
            </table>
            <Loader {...pageStatus} />
        </>
    );
}

export { Index };
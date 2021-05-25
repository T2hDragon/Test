import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import Loader from "../../components/Loader";
import { IRequirement } from "../../dto/IRequirement";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";



const RowDisplay = (props: { requirement: IRequirement }) => (
    <tr>
        <td>
            {props.requirement.name}
        </td>
        <td>
            {props.requirement.description}
        </td>
        <td>
            {props.requirement.price}
        </td>
        <td className="float-right">
            <Link to={'/requirements/' + props.requirement.id}>Details</Link>
            {useContext(AppContext).jwt === null ?
                <></>
                :
                <>
                    <span> | </span>
                    <Link to={'/requirements/edit/' + props.requirement.id}>Edit</Link>
                </>
            }
        </td>
    </tr>
);

const Index = () => {
    const [requirements, setRequirements] = useState([] as IRequirement[]);

    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.getAll<IRequirement>('/requirements');

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setRequirements(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            <h1>Requirements</h1>
            <Link to={'/requirements/create'}>Create new requirement</Link>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                        <th>
                            description
                        </th>
                        <th>
                            Price
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {requirements.map(requirement =>
                        <RowDisplay requirement={requirement} key={requirement.id} />)
                    }
                </tbody>
            </table>
            <Loader {...pageStatus} />
        </>
    );
}

export { Index };
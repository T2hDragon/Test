import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import { IRequirement } from "../../dto/IRequirement";
import Loader from "../../components/Loader";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";

const initialRequirementValues: IRequirement = {
    id: '',
    name: '',
    nameId: '',
    description: '',
    descriptionId: '',
    amount: 0,
    price: 0,
};

const Details = () => {
    const appState = useContext(AppContext);
    const { id } = useParams() as IRouteId;
    const [requirement, setRequirement] = useState(initialRequirementValues);


    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.get<IRequirement>('/requirements', id, appState.jwt!);

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setRequirement(result.data);
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
                <h4>Requirement</h4>
                <hr />
                <Loader {...pageStatus} />

                <dl className="row">
                    <dt className="col-sm-4">Id</dt>
                    <dd className="col-sm-8">
                        {requirement.id}
                    </dd>

                    <dt className="col-sm-4">Name</dt>
                    <dd className="col-sm-8">
                        {requirement.name}
                    </dd>
                    <dt className="col-sm-4">Description</dt>
                    <dd className="col-sm-8">
                        {requirement.description}
                    </dd>
                    <dt className="col-sm-4">Price</dt>
                    <dd className="col-sm-8">
                        {requirement.price}
                    </dd>
                    {requirement.amount !== null ?
                        <>
                            <dt className="col-sm-4">Amount</dt>
                            <dd className="col-sm-8">
                                {requirement.amount}
                            </dd>
                        </>
                        :
                        <></>
                    }
                </dl>
            </div>
            <div>
                <Link
                    to="/requirements"
                >Back to list</Link>
            </div >
        </>
    );
}

export { Details };
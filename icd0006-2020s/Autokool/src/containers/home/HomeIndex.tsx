import { useEffect, useState, useContext } from "react";
import { IContract } from "../../dto/IContract";
import { ContractService } from "../../services/contract-service";
import { AppContext } from "../../context/AppContext";
import { EPageStatus } from "../../types/EPageStatus";
import Loader from "../../components/Loader";
import { useHistory } from "react-router-dom";


const Index = () => {
    const [ActiveContracts, setActiveContracts] = useState([] as IContract[]);
    const [InviteContracts, setInviteContracts] = useState([] as IContract[]);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.home.homeIndex;

    const history = useHistory();

    const LoadContracts = async () => {
        if (appState.jwt === null) return;
        appState.title = null
        appState.contractId = null
        appState.setAuthInfo(appState.jwt, appState.userName, appState.firstName, appState.lastName, appState.title, appState.contractId, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
        let result = await ContractService.getAll(appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setActiveContracts(result.data.filter(e => e.status === "active"));
            setInviteContracts(result.data.filter(e => e.status === "invited"));
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });

        }
    }

    useEffect(() => {
        LoadContracts();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    const ContractClicked = async (e: Event, contractId: string) => {
        let response = await ContractService.get(contractId, appState.currentLanguage.name, appState.jwt!);
        if (!response.ok) {
        } else {
            appState.title = response.data!.title;
            appState.contractId = response.data!.contractId;
            appState.setAuthInfo(appState.jwt, appState.userName, appState.firstName, appState.lastName, appState.title, appState.contractId, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
            const pushUrl = appState.title === "teacher" ? "/teacher/" + response.data!.contractId + "/overview"
                : appState.title === "student" ? "/student/courses/" + response.data!.contractId
                    : "DefaultUrl"
            history.push(pushUrl);
        }
    }

    const InviteClickedClicked = async (e: Event, contractId: string, accepted: boolean) => {
        let response = await ContractService.InviteHandle(contractId, accepted, appState.currentLanguage.name, appState.jwt!);
        if (!response.ok) {
        } else {
            setInviteContracts(InviteContracts.filter(e => e.contractId !== contractId));
            if (accepted) {
                const acceptedContract = InviteContracts.find(e => e.contractId === contractId)
                ActiveContracts.push(acceptedContract!);
                setActiveContracts(ActiveContracts);
            }
            history.push("/");
        }
    }

    if (appState.jwt != null) {
        return (
            <>
                <h1 className="text-center">{resource.courses}</h1>
                <Loader {...pageStatus} />
                {ActiveContracts.map(contract =>

                    <div key={contract.contractId} className="bg-info btn inline-table" onClick={(event) => ContractClicked(event.nativeEvent, contract.contractId)}>
                        <h2 className="course-school text-center">{contract.drivingSchoolName}</h2>
                        <h4 className="course-title text-center">{contract.title === "teacher" ? resource.teacher
                            : contract.title === "student" ? resource.student
                                : "Default Title"}
                        </h4>
                    </div>
                )}

                {(InviteContracts.length !== 0) ?
                    <>
                        <br></br><br></br><br></br><br></br><br></br><br></br>

                        <h1 className="text-center">{resource.invites}</h1>
                        {InviteContracts.map(contract =>

                            <div key={contract.contractId} className="bg-info inline-table">
                                <h2 className="course-school text-center">{contract.drivingSchoolName}</h2>
                                <h4 className="course-title text-center">{contract.title === "teacher" ? resource.teacher
                                    : contract.title === "student" ? resource.student
                                        : "Default Title"}</h4>
                                <i className="fa fa-check btn text-primary btn-font" onClick={(event) =>
                                    InviteClickedClicked(event.nativeEvent,
                                        contract.contractId,
                                        true
                                    )}
                                ></i>
                                <i className="fa fa-times-circle btn float-right text-danger btn-font" onClick={(event) =>
                                    InviteClickedClicked(event.nativeEvent,
                                        contract.contractId,
                                        false)}
                                ></i>
                            </div>
                        )}
                    </>
                    : null}
            </>
        )
    }
    return (
        <>
            <h1>{resource.title}</h1>
            <br />
            <div>{resource.description}</div>
        </>
    );
}

export default Index;
import { useEffect, useState } from 'react';
import { Route, Switch } from 'react-router-dom';
import { LangService } from './services/lang-service';
import Footer from './components/Footer';
import Header from './components/Header';
import HomeIndex from './containers/home/HomeIndex';
import Login from './containers/identity/Login';
import Register from './containers/identity/Register';
import Page404 from './containers/Page404';
import { AppContextProvider, initialAppState } from './context/AppContext';
import { ILangResources } from "./dto/ILangResources";
import { ISupportedLanguage } from "./dto/ISupportedLanguage";

import { Courses as StudentCourses } from './containers/students/StudentCourses';
import { Index as StudentCourse } from './containers/students/course/CourseIndex';
import { Schedule as StudentCourseSchedule } from './containers/students/course/CourseSchedule';


import { TeacherOverview } from './containers/teachers/TeacherOverview';
import { StudentsIndex } from './containers/teachers/students/StudentsIndex';
import { TeacherStudentCourses } from './containers/teachers/students/TeacherStudentCourses';
import { TeacherStudentCourse } from './containers/teachers/students/TeacherStudentCourse';
import { TeacherSchedule } from './containers/teachers/TeacherSchedule';
import { TeacherStudentCourseSchedule } from './containers/teachers/students/TeacherStudentCourseSchedule';

function App() {
    const setAuthInfo = (jwt: string | null, userName: string, firstName: string, lastName: string, title: string | null, contractId: string | null, supportedLanguages: ISupportedLanguage[], currentLanguage: ISupportedLanguage, langResources: ILangResources): void => {
        setAppState({ ...appState, jwt, userName, firstName, lastName, title, contractId, supportedLanguages, currentLanguage, langResources });
    }
    const [appState, setAppState] = useState({ ...initialAppState, setAuthInfo });

    const LoadSupportedLanguages = async () => {
        let response = await LangService.getSupportedLanguages();
        if (response.ok && response.data) {
            appState.supportedLanguages = response.data as ISupportedLanguage[];
            appState.setAuthInfo(appState.jwt, appState.userName, appState.firstName, appState.lastName, appState.title, appState.contractId, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
        } else {
        }
    }

    const LoadLangResources = async () => {
        let response = await LangService.getLangResources(appState.currentLanguage.name);
        if (response.ok && response.data) {
            appState.langResources = response.data as ILangResources;
            appState.setAuthInfo(appState.jwt, appState.userName, appState.firstName, appState.lastName, appState.title, appState.contractId, appState.supportedLanguages, appState.currentLanguage, appState.langResources);
        } else {
        }
    }

    useEffect(() => {
        LoadLangResources();
        LoadSupportedLanguages();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <AppContextProvider value={appState} >
                <Header />
                <div className="container">
                    <main role="main" className="pb-3">
                        <Switch>
                            <Route exact path="/" component={HomeIndex} />

                            <Route path="/identity/login" component={Login} />
                            <Route path="/identity/register" component={Register} />

                            <Route path="/student/courses/:id" component={StudentCourses} />
                            <Route path="/student/course/:id" component={StudentCourse} />
                            <Route path="/student/courseSchedule/:id" component={StudentCourseSchedule} />

                            <Route path="/teacher/:teacherId/overview" component={TeacherOverview} />
                            <Route path="/teacher/:teacherId/schedule" component={TeacherSchedule} />
                            <Route path="/teacher/:teacherId/students" component={StudentsIndex} />
                            <Route path="/teacher/:teacherId/student/:studentId/course/:studentCourseId/schedule" component={TeacherStudentCourseSchedule} />
                            <Route path="/teacher/:teacherId/student/:studentId/course/:studentCourseId" component={TeacherStudentCourse} />
                            <Route path="/teacher/:teacherId/student/:studentId" component={TeacherStudentCourses} />

                            <Route component={Page404} />
                        </Switch>
                    </main>
                </div>
                <Footer />
            </AppContextProvider>
        </>
    );
}

export default App;

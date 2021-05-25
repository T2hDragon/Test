import React from "react";
import { ILangResources } from "../dto/ILangResources";
import { ISupportedLanguage } from "../dto/ISupportedLanguage";

export interface IAppState {
    jwt: string | null;
    userName: string;
    firstName: string;
    lastName: string;
    title: string | null;
    contractId: string | null;
    supportedLanguages: ISupportedLanguage[];
    currentLanguage: ISupportedLanguage;
    langResources: ILangResources;
    setAuthInfo: (jwt: string | null, username: string, firstName: string, lastName: string, title: string | null, contractId: string | null, supportedLanguages: ISupportedLanguage[], currentLanguage: ISupportedLanguage, langResources: ILangResources) => void;
}

export const initialAppState : IAppState = {
    jwt: null,
    userName: '',
    firstName: '',
    lastName: '',
    title: null,
    contractId: null,
    supportedLanguages: [],
    currentLanguage: { name: 'et', nativeName: 'eesti' },
    langResources: {
      "frontEnd": {
        "components": {
          "alert": {},
          "footer": {
            "appName": "Sõidkooli app"
          },
          "header": {
            "brand": "Autokool",
            "courses": "Kursused",
            "logIn": "Logi sisse",
            "logOut": "Logi välja",
            "overview": "Ülevaade",
            "register": "Registreeri",
            "schedule": "Ajakava",
            "students": "Õpilased"
          },
          "loader": {
            "error": "Viga",
            "loading": "Laeb"
          }
        },
        "containers": {
          "home": {
            "homeIndex": {
              "description": "Autokoolide veebisait töökoormuse digitaliseerimiseks. Nagu sõidutundide märkimine ja ülevaade õpilaste edusammudest. ",
              "invites": "Kutsed",
              "courses": "Kursused",
              "student": "Õpilane",
              "teacher": "Õpetaja",
              "title": "Sõidukooli haldamise lehekülg"
            }
          },
          "identity": {
            "login": {
              "emptyInputs": "Tühi kasutajanimi või parool",
              "logIn": "Logi sisse",
              "password": "Parool",
              "username": "Kasutajanimi"
            },
            "register": {
              "username": "Kasutajanimi",
              "email": "Email",
              "emptyInputs": "Tühi kasutajanimi või parool",
              "firstname": "Eesnimi",
              "lastname": "Perenimi",
              "password": "Parool",
              "passwordConfirmation": "Parooli kinnitus",
              "passwordDoesntMatch": "Paroolid ei kattu",
              "registerButton": "Registreeri",
              "unableToLogIn": "Ebaõnnestus sisse logida"
            }
          },
          "student": {
            "course": {
              "courseIndex": {
                "complete": "Läbitud",
                "drivingLessons": "Sõidutunnid",
                "progress": "Edenemine"
              },
              "courseSchedule": {
                "complete": "Läbitud",
                "backToOverview": "Tagasi",
                "day": "Päev",
                "drivingLessons": "Sõidutunnid",
                "month": "Kuu",
                "progress": "Edenemine",
                "teacher": "Õpetaja",
                "time": "Aeg",
                "year": "Aasta"
              }
            },
            "studentCourses": {
              "category": "Kategooria",
              "courses": "Kursused"
            }
          },
          "teacher": {
            "teacherOverview": {
              "course": "Kursus",
              "dailyLessons": "Päeva sõidutunnid",
              "nextLesson": "Järgmine tund",
              "restOfTheDay": "Ülejäänud päev",
              "student": "Õpilane",
              "time": "Kellaaeg"
            },
            "teacherSchedule": {
              "lessonRemoved": "Sõidutund on eemaldatud",
              "lessons": "Sõidutunnid",
              "remove": "Eemalda",
              "removedStudent": "Eemaldatud õpilane",
              "student": "Õpilane",
              "time": "Kellaaeg"
            },
            "students": {
              "studentsIndex": {
                "email": "Email",
                "filter": "Filtreeri",
                "invite": "Kutsu",
                "name": "Nimi",
                "schoolStudents": "Kooli õpilased",
                "username": "Kasutajanimi"
              },
              "teacherStudentCourse": {
                "complete": "Läbitud",
                "drivingLessons": "Sõidutunnid",
                "delete": "Kustutua",
                "progress": "Edenemine",
                "reportUpdated": "Raport uuendatud",
                "update": "Uuenda"
              },
              "teacherStudentCourses": {
                "category": "Kategooria",
                "courses": "kursused",
                "kick": "Viska välja",
                "addCourse": "Lisa kursus"
              },
              "teacherStudentCourseSchedule": {
                "add": "Lisa",
                "addLesson": "Lisa sõidutund",
                "backToStudentCourse": "Tagasi",
                "complete": "Läbitud",
                "day": "Kuupäev",
                "drivingLessons": "Sõidutunnid",
                "lessonLength": "Sõidutunni pikkus",
                "lessonLengthError": "Sõidutunni pikkus peab olema kauem kui ",
                "lessonRemoved": "Sõidutund eemaldatud",
                "month": "Kuu",
                "nextLesson": "Järgmine sõidutund",
                "progress": "Edenemine",
                "remove": "Eemalda",
                "student": "Õpilane",
                "studentLessons": "Õpilase sõidutunnid",
                "teacher": "Õpetaja",
                "lessonAdded": "Kursus lisatud!",
                "teacherLessons": "Õpetaja sõidutunnid",
                "time": "Kellaaeg",
                "year": "Aasta"
              }
            }
          },
          "page404": {
            "notFound": "Lehekülg ei leitud"
          }
        }
      }
    },

    setAuthInfo: (jwt: string | null, username: string, firstName: string, lastName: string, title: string | null, contractId: string | null, supportedLanguages: ISupportedLanguage[], currentLanguage: ISupportedLanguage, langResources: ILangResources): void => {},

}

export const AppContext = React.createContext<IAppState>(initialAppState);
export const AppContextProvider = AppContext.Provider;
export const AppContextConsumer = AppContext.Consumer;

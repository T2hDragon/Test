export interface ILangResources {
    "frontEnd": {
      "components": {
        "alert": {},
        "footer": {
          "appName": string
        },
        "header": {
          "brand": string,
          "courses": string,
          "logIn": string,
          "logOut": string,
          "overview": string,
          "register": string,
          "schedule": string,
          "students": string,
        },
        "loader": {
          "error": string,
          "loading": string,
        }
      },
      "containers": {
        "home": {
          "homeIndex": {
            "description": string,
            "invites": string,
            "courses": string,
            "student": string,
            "teacher": string,
            "title": string,
          }
        },
        "identity": {
          "login": {
            "emptyInputs": string,
            "logIn": string,
            "password": string,
            "username": string,
          },
          "register": {
            "username": string,
            "email": string,
            "emptyInputs": string,
            "firstname": string,
            "lastname": string,
            "password": string,
            "passwordConfirmation": string,
            "passwordDoesntMatch": string,
            "registerButton": string,
            "unableToLogIn": string,
          }
        },
        "student": {
          "course": {
            "courseIndex": {
              "complete": string,
              "drivingLessons": string,
              "progress": string,
            },
            "courseSchedule": {
              "complete": string,
              "backToOverview": string,
              "day": string,
              "drivingLessons": string,
              "month": string,
              "progress": string,
              "teacher": string,
              "time": string,
              "year": string,
            }
          },
          "studentCourses": {
            "category": string,
            "courses": string,
          }
        },
        "teacher": {
          "teacherOverview": {
            "course": string,
            "dailyLessons": string,
            "nextLesson": string,
            "restOfTheDay": string,
            "student": string,
            "time": string
          },
          "teacherSchedule": {
            "lessonRemoved": string,
            "lessons": string,
            "remove": string,
            "removedStudent": string,
            "student": string,
            "time": string
          },
          "students": {
            "studentsIndex": {
              "email": string,
              "filter": string,
              "invite": string,
              "name": string,
              "schoolStudents": string,
              "username": string
            },
            "teacherStudentCourse": {
              "complete": string,
              "drivingLessons": string,
              "delete": string,
              "progress": string,
              "reportUpdated": string,
              "update": string
            },
            "teacherStudentCourses": {
              "category": string,
              "addCourse": string,
              "courses": string,
              "kick": string
            },
            "teacherStudentCourseSchedule": {
              "add": string,
              "addLesson": string,
              "backToStudentCourse": string,
              "complete": string,
              "drivingLessons": string,
              "day": string,
              "lessonLength": string,
              "lessonAdded": string,
              "lessonLengthError": string,
              "lessonRemoved": string,
              "month": "Kuu",
              "nextLesson": string,
              "progress": string,
              "remove": string,
              "student": string,
              "studentLessons": string,
              "teacher": string,
              "teacherLessons": string,
              "time": string,
              "year": string
            }
          }
        },
        "page404": {
          "notFound": string,
        }
      }
    }
  }
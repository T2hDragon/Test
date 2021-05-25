import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/identity/Login.vue';
import Register from '@/views/identity/Register.vue';

import TitlesIndex from '@/views/Titles/Index.vue';
import TitlesDetails from '@/views/Titles/Details.vue';
import TitlesEdit from '@/views/Titles/Edit.vue';
import TitlesCreate from '@/views/Titles/Create.vue';

import StatusesIndex from '@/views/Statuses/Index.vue';
import StatusesDetails from '@/views/Statuses/Details.vue';
import StatusesEdit from '@/views/Statuses/Edit.vue';
import StatusesCreate from '@/views/Statuses/Create.vue';

import RequirementsIndex from '@/views/Requirements/Index.vue';
import RequirementsDetails from '@/views/Requirements/Details.vue';
import RequirementsEdit from '@/views/Requirements/Edit.vue';
import RequirementsCreate from '@/views/Requirements/Create.vue';

import NotFound from '@/views/NotFound.vue'

const routes: Array<RouteRecordRaw> = [
    { path: '/', name: 'Home', component: Home },

    // ========================== ACCOUNT ==============================================

    { path: '/Account/Register', name: 'Register', component: Register },
    { path: '/Account/Login', name: 'Login', component: Login },

    // ========================== TITLES ====================================

    { path: '/titles', name: 'TitlesIndex', component: TitlesIndex },
    { path: '/titles/:id', name: 'TitlesDetails', component: TitlesDetails, props: true },
    { path: '/titles/create', name: 'TitlesCreate', component: TitlesCreate },
    { path: '/titles/edit/:id', name: 'TitlesEdit', component: TitlesEdit, props: true },
    // ========================== STATUSES ====================================

    { path: '/statuses', name: 'StatusesIndex', component: StatusesIndex },
    { path: '/statuses/:id', name: 'StatusesDetails', component: StatusesDetails, props: true },
    { path: '/statuses/create', name: 'StatusesCreate', component: StatusesCreate },
    { path: '/statuses/edit/:id', name: 'StatusesEdit', component: StatusesEdit, props: true },
    // ========================== REQUIREMENTS ====================================

    { path: '/requirements', name: 'RequirementsIndex', component: RequirementsIndex },
    { path: '/requirements/:id', name: 'RequirementsDetails', component: RequirementsDetails, props: true },
    { path: '/requirements/create', name: 'RequirementsCreate', component: RequirementsCreate },
    { path: '/requirements/edit/:id', name: 'RequirementsEdit', component: RequirementsEdit, props: true },

    { path: "/:catchAll(.*)", name: "NotFound", component: NotFound },
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router

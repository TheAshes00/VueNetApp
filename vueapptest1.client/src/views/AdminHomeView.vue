<script>
import { useAdminStore } from '@/stores/admin';
import { useStudentStore } from '@/stores/student';

export default {
    setup(){
        const studentStore = useStudentStore();
        const adminStore = useAdminStore();
        return { studentStore, adminStore }
    },
    methods: {
        closeAdminSession(){
            this.studentStore.$reset(); 
            this.adminStore.$reset();
            this.$router.push('/')
        }
    }
}
</script>
<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container-fluid">
            <a class="navbar-brand" v-on:click="closeAdminSession">Logout</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDarkDropdown" aria-controls="navbarNavDarkDropdown" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavDarkDropdown">
                <ul class="navbar-nav">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDarkDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Workshops
                        </a>
                        <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="navbarDarkDropdownMenuLink">
                            <li><RouterLink to="/admin/workshop/Workshop">Add Workshop</RouterLink></li>
                            <li><RouterLink to="/admin/workshop/Tutor">Add Tutor</RouterLink></li>
                            <li><RouterLink to="/admin/workshop/WorkshopTutor">Add Workshop Tutor</RouterLink></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="collapse navbar-collapse" id="navbarNavDarkDropdown1">
                <ul class="navbar-nav">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDarkDropdownMenuLink1" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Reports
                        </a>
                        <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="navbarDarkDropdownMenuLink1">
                            <li><RouterLink to="/admin/report/Student">By Student</RouterLink></li>
                            <li><RouterLink to="/admin/report/Tutor">By Tutor</RouterLink></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="collapse navbar-collapse" id="navbarNavDarkDropdown2">
                <ul class="navbar-nav">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDarkDropdownMenuLink2" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Inventory
                        </a>
                        <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="navbarDarkDropdownMenuLink3">
                            <li><RouterLink to="/admin/inventory/Material">Manage material</RouterLink></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div>
        <RouterView></RouterView>
    </div>
</template>
<script>
import { defineAsyncComponent } from 'vue';
import LoaderComponent from '@/components/LoaderComponent.vue';

export default {
  methods: {
    goToRegister() {
      this.$router.push('/register')
    },
  },
  components:{
    LoginComponent : defineAsyncComponent(
        {
            //                                              // the loader function
            loader: () => import ('@/components/LoginComponent.vue'),

            //                                              // A component to use while 
            //                                              // the async component is loading
            loadingComponent: LoaderComponent,
            //                                              // Delay before showing the 
            //                                              // loading component. Default: 200ms.
            delay: 200,

            // // A component to use if the load fails
            // errorComponent: ErrorComponent,
            // // The error component will be displayed if a timeout is
            // // provided and exceeded. Default: Infinity.
            // timeout: 3000
        }
    ),

  }
}
</script>

<template>
    <LoginComponent/>
    <div class="link">
      <RouterLink to="/register" class="shadow-box">¿Primera vez? Registrate</RouterLink>
    </div>
</template>

<style scoped>
.link {
  display: flex;
  justify-content: center;
  padding: 0px 16px;
  margin: 16px 26px;
}

a{
  border-radius: 5px;
  color: #c7ffc2;
  background: rgb(0, 0, 0, .6);
  padding: 8px 16px;
}
</style>
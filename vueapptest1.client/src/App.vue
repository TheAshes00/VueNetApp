<template>
   <div :style="$route.path != '/' ? objStyle : {}">
    <KeepAlive>
      <main>
        <RouterView />
      </main>
    </KeepAlive>
   </div>
</template>

<script>
export default {
  data(){
    return{
      objStyle: {
        'z-index': '1',
        'position': 'relative',
        'background': 'rgb(0 0 0 / 65%)',
        'height': '100%',
        'padding': '0 2em',
      }
    }
  }
}
</script>

<style>
.logo {
  display: block;
  margin: 0 auto 2rem;
}

#app {
  background-image: url(./assets/banner.jpg);
  height: 100vh;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  overflow: hidden;
  
}

@media (min-width: 1024px) {
  .logo {
    margin: 0 2rem 0 0;
  }

}
</style>

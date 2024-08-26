<template>
    <header v-if="$route.path != '/'">
        <nav>
        <RouterLink to="/">Home</RouterLink>
        </nav>
    </header>
</template>

<style>
header {
  line-height: 1.5;
}

@media (min-width: 1024px) {
  header {
    width: 100%;
    /* color: #f2f2f2; */
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
    font-size: 17px;
    display: flex;
    justify-content: flex-end;
  }
}
</style>
<template>
  <nav-menu></nav-menu>
  <harbor-status />
</template>

<script>
    import NavMenu from './components/NavMenu.vue';
    import HarborStatus from './components/HarborStatus.vue';

    export default {
        name: 'App',
        components: {
            NavMenu,
            HarborStatus
        }
    }
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>

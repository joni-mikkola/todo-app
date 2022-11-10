<template>
    <div class="container">
        <div class="row justify-content-md-center">
            <Todo />
        </div>
    </div>

</template>

<script>
import Todo from "./components/Todo.vue";

export default {
    name: "App",
    components: {
        Todo,
    },
};
</script>

<style>
#app {
    margin-top: 60px;
}
</style>

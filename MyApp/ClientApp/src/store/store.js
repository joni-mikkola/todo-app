import { createStore } from "vuex";
import axios from "axios";
import { useToast } from "vue-toastification";

const toast = useToast();

const baseUrl = process.env.VUE_APP_BASE_URL;
axios.defaults.baseURL = baseUrl;

import moment from "moment";

export const store = createStore({
  state: {
    error: null,
    todos: [],
  },
  actions: {
    getTodos({ commit }) {
      commit("LOADING", true);
      return axios
        .get("api/todo")
        .then((response) => {
          commit("GET_TODOS", response.data);
        })
        .catch((error) => {
          toast.error(error.message);
        })
        .finally(() => {
          commit("LOADING", false);
        });
    },
    postTodo({ commit }, values) {
      const date = moment(values.date).utcOffset(0, true).toISOString();
      commit("LOADING", true);
      return axios
        .post("api/todo", {
          date: date,
          message: values.todo,
        })
        .then(() => {
          toast.success("Todo saved succesfully!");
          this.dispatch("getTodos");
          return Promise.resolve();
        })
        .catch((error) => {
          try {
            if (error.response.status == 400) {
              let errors = [];
              for (let e in error.response.data.errors) {
                errors.push(error.response.data.errors[e]);
              }
              toast.error(errors.join("\n"));
            }
          } catch (exception) {
            toast.error(error.message);
          }
          return Promise.reject(error);
        })
        .finally(() => {
          commit("LOADING", false);
        });
    },
    deleteTodo({ commit }, id) {
      commit("LOADING", true);
      return axios
        .delete(`api/todo/${id}`)
        .then(() => {
          toast.success("Todo deleted succesfully!");
          return Promise.resolve();
        })
        .catch((error) => {
          toast.error(error.message);
          return Promise.reject(error);
        })
        .finally(() => {
          commit("LOADING", false);
        });
    },
    toggleTodo(commit, id, done) {
      return axios
        .put(`api/todo/${id}`, {
          done: done,
        })
        .then((response) => {
          return Promise.resolve(response);
        })
        .catch((error) => {
          toast.error(error.message);
          return Promise.reject(error);
        });
    },
    toggleBackend(commit, value) {
      if (value) {
        axios.defaults.baseURL = baseUrl;
      } else {
        axios.defaults.baseURL = "https://38q756098567beryipobnueproy.com";
      }
    },
  },
  mutations: {
    GET_TODOS(state, todos) {
      state.todos = todos;
    },
    LOADING(state, loading) {
      state.loading = loading;
    },
  },
});

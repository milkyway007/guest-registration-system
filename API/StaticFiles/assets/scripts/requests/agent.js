import {
  showMessage,
  showValidationError,
  showServerError,
} from "./../message.js";
const sleep = (delay) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};
axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.response.use(
  async (response) => {
    await sleep(1000);
    return response;
  },
  (error) => {
    const { data, status } = error.response;
    console.log(error);
    switch (status) {
      case 400:
        if (data.errors) {
          const modalStateErrors = [];
          for (const key in data.errors) {
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          showValidationError(modalStateErrors.flat());
        } else {
          showMessage(data);
        }
        break;
      case 404:
        showMessage("Otsisime kõikjal ega leidnud midagi.");
        break;
      case 500:
        showServerError(error);
        break;
    }

    return Promise.reject(error);
  }
);

const headers = {
  headers: {
    "Content-Type": "application/json",
  },
};

const responseBody = (response) => {
  console.log(response);
  console.log(response.data);

  return response.data;
};

const requests = {
  events: {
    get: (url) => axios.get(url).then(responseBody),
    post: (url, body) => axios.post(url, body).then(responseBody),
    delete: (url) => axios.delete(url).then(responseBody),
  },

  participats: {
    get: (url) => axios.get(url).then(responseBody),
    post: (url, body) => axios.post(url, body).then(responseBody),
    put: (url, body) => axios.put(url, body).then(responseBody),
    delete: (url) => axios.delete(url).then(responseBody),
  },
};

const events = {
  listPast: () => requests.events.get("/events?predicate=past"),
  listFuture: () => requests.events.get("/events?predicate=future"),
  create: (body) => requests.events.post("/events", body),
  delete: (id) => requests.events.delete(`/events/${id}`),
};

const participants = {
  listPersons: (eventId) =>
    requests.participats.get(`/events/${eventId}/persons`),
  listCompanies: (eventId) =>
    requests.participats.get(`/events/${eventId}/companies`),
  createPerson: (eventId, body) =>
    requests.participats.post(`/events/${eventId}/persons`, body, headers),
  createCompany: (eventId, body) =>
    requests.participats.post(`/events/${eventId}/companies`, body, headers),
  editPerson: (body) =>
    requests.participats.put(`/participants/persons/${body.code}`, body),
  editCompany: (body) =>
    requests.participats.put(`/participants/companies/${body.code}`, body),
  delete: (eventId, participantCode) =>
    requests.participats.delete(
      `/events/${eventId}/participants/${participantCode}`
    ),
};

const agent = {
  events: events,
  participants: participants,
};

export default agent;

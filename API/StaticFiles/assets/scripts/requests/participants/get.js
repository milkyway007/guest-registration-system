import { loadParticipants } from "../../participants/participant_list.js";
import agent from "./../agent.js";

const loadEventPersons = (eventId) => {
  agent.participants
    .listPersons(eventId)
    .then((response) => {
      loadParticipants(response, "person");
    })
    .catch(function (error) {
      console.log(error);
    });
};

const loadEventCompanies = (eventId) => {
  agent.participants
    .listCompanies(eventId)
    .then((response) => {
      loadParticipants(response, "company");
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { loadEventPersons, loadEventCompanies };

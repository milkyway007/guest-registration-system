import { closeParticipantDetailsModal } from "../../participants/participant_details_modal.js";
import { updateUiAfterEventUpdate } from "../../events/event_add.js";
import agent from "./../agent.js";

const postPerson = (eventId, person) => {
  agent.participants
    .createPerson(eventId, person)
    .then(function (response) {
      updateUiAfterEventUpdate(eventId, person);
      closeParticipantDetailsModal();
    })
    .catch(function (error) {
      console.log(error);
    });
};

const postCompany = (eventId, company) => {
  agent.participants
    .createCompany(eventId, company)
    .then(function (response) {
      updateUiAfterEventUpdate(eventId, company);
      closeParticipantDetailsModal();
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { postPerson, postCompany };

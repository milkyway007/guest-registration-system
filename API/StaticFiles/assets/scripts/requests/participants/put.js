import {
  updateUiAfterParticipatModify,
  closeViewParticipantModal,
} from "../../participants/participant_modify.js";
import agent from "./../agent.js";

const putPerson = (participant) => {
  return agent.participants
    .editPerson(participant)
    .then(function (response) {
      updateUiAfterParticipatModify(participant, "person");
      closeViewParticipantModal();
    })
    .catch(function (error) {
      console.log(error);
    });
};

const putCompany = (participant) => {
  console.log(participant);
  agent.participants
    .editCompany(participant)
    .then(function (response) {
      updateUiAfterParticipatModify(participant, "company");
      closeViewParticipantModal();
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { putPerson, putCompany };

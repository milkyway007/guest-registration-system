import {
  updateUiAfterParticipantdelete,
  closeDeleteParticipantModal,
} from "./../../participants/participant_delete.js";
import agent from "./../agent.js";

const deleteParticipant = (eventId, participantCode) => {
  agent.participants
    .delete(eventId, participantCode)
    .then(function (response) {
      updateUiAfterParticipantdelete(participantCode);
      closeDeleteParticipantModal();
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { deleteParticipant };

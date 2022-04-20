import {
  updateUiAfterEventEdelete,
  closeDeleteEventModal,
} from "../../events/event_delete.js";
import agent from "./../agent.js";

const deleteEvent = (eventId) => {
  console.log(eventId);
  agent.events
    .delete(eventId)
    .then(function (response) {
      console.log(response);
      updateUiAfterEventEdelete(response);
      closeDeleteEventModal();
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { deleteEvent };

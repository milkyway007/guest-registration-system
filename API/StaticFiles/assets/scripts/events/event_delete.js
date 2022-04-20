const deleteEventModal = document.getElementById("delete-event-modal");
import { findEventIndex, deleteEventAtIndex } from "./event_container.js";
import { deleteEvent } from "../requests/events/delete.js";

const closeDeleteEventModal = () => {
  if (!deleteEventModal) {
    return;
  }

  deleteEventModal.classList.remove("is-active");
};

const updateUiAfterEventEdelete = (eventId) => {
  let eventIndex = findEventIndex(eventId);
  console.log(eventIndex);
  deleteEventAtIndex(eventIndex);

  const listRoot = document.getElementById("event-list");
  console.log(listRoot.children[eventIndex]);
  listRoot.removeChild(listRoot.children[eventIndex]);
};

const deleteEventHandler = (eventId) => {
  deleteEvent(eventId);
};

const cancelDeleteEventModalHandler = () => {
  closeDeleteEventModal();
};

const showDeleteEventModalHandler = (id) => {
  console.log(id);
  deleteEventModal.classList.add("is-active");

  const cancelDeleteEventButton = document.getElementById(
    "cancel-delete-event-button"
  );
  let deleteEventButton = document.getElementById("delete-event-button");
  deleteEventButton.replaceWith(deleteEventButton.cloneNode(true));
  deleteEventButton = document.getElementById("delete-event-button");

  cancelDeleteEventButton.removeEventListener(
    "click",
    cancelDeleteEventModalHandler
  );

  cancelDeleteEventButton.addEventListener(
    "click",
    cancelDeleteEventModalHandler
  );
  deleteEventButton.addEventListener(
    "click",
    deleteEventHandler.bind(null, id)
  );
};

export {
  showDeleteEventModalHandler,
  updateUiAfterEventEdelete,
  closeDeleteEventModal,
};

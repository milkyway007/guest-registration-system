import { showDeleteEventModalHandler } from "./event_delete.js";
import { showAddParticipantModalHandler } from "../participants/participant_add.js";

const eventRoot = document.getElementById("event-list");

const updateEventParticipantCountAtIndex = (eventIndex, participantCount) => {
  const eventElement = eventRoot.children[eventIndex];
  console.log(eventElement);
  const participantCountElement =
    eventElement.querySelector(".participant-count");

  participantCountElement.innerHTML =
    participantCount === 1
      ? `${participantCount} osaleja`
      : `${participantCount} osalejat`;
};

const renderEvent = (event) => {
  var eventElement = event.render();

  const startDeleteEventButton = eventElement.querySelector(
    ".start-delete-event-button"
  );
  const startAddParticipantButton = eventElement.querySelector(
    ".start-add-participant-button"
  );

  startDeleteEventButton.addEventListener(
    "click",
    showDeleteEventModalHandler.bind(null, event.id)
  );
  startAddParticipantButton.addEventListener(
    "click",
    showAddParticipantModalHandler.bind(null, event.id)
  );

  eventRoot.appendChild(eventElement);
};

const emptyEventRoot = () => {
  removeChildren(eventRoot);
};

const renderEvents = (events) => {
  emptyEventRoot(events);
  for (const event of events) {
    renderEvent(event);
  }
};

export {
  renderEvent,
  renderEvents,
  emptyEventRoot,
  updateEventParticipantCountAtIndex,
};

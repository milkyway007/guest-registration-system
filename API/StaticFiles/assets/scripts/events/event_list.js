import { Event, Address } from "../model.js";
import {
  emptyContainer,
  addEvent,
  renderSortedEvents,
} from "./event_container.js";

const eventRoot = document.getElementById("event-list");
const futureEventsLinkContainer = document.getElementById(
  "future-events-link-container"
);
const pastEventsLinkContainer = document.getElementById(
  "past-events-link-container"
);

const toggleEventTypes = () => {
  futureEventsLinkContainer.classList.toggle("is-active");
  pastEventsLinkContainer.classList.toggle("is-active");
};

const loadEvents = (data) => {
  toggleEventTypes();
  emptyContainer();
  removeChildren(eventRoot);

  for (const obj of data) {
    const address = new Address(
      obj.address.id,
      obj.address.line1,
      obj.address.line2,
      obj.address.city,
      obj.address.state,
      obj.address.country,
      obj.address.zip
    );
    const event = new Event(
      obj.id,
      obj.name,
      new Date(obj.occurrence),
      obj.description,
      address,
      obj.participants
    );

    addEvent(event);
  }

  renderSortedEvents();
};

export { loadEvents };

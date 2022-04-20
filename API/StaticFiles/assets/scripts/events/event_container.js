import { renderEvents } from "./event_renderer.js";
import { Event, Address } from "../model.js";

let events = [];

const getEvent = (eventIndex) => {
  return events[eventIndex];
};

const deleteEventAtIndex = (index) => {
  events.splice(index, 1);
};

const findEventIndex = (eventId) => {
  let eventIndex = 0;
  for (const event of events) {
    if (event.id === eventId) {
      break;
    }
    eventIndex++;
  }

  return eventIndex;
};

const sort = (a, b) => {
  if (new Date(a.occurence) > new Date(b.occurence)) {
    return 1;
  }
  if (new Date(a.occurence) < new Date(b.occurence)) {
    return -1;
  }

  return 0;
};

const emptyContainer = () => {
  events = [];
};

const addEvent = (event) => {
  events.push(event);
};

const renderSortedEvents = () => {
  events.sort(sort);
  renderEvents(events);
};

const addEventAndRender = (obj) => {
  const pastEventsLink = document.getElementById("past-events-link-container");
  console.log(
    "pastEventsLink.classList.contains " +
      pastEventsLink.classList.contains("is-active")
  );
  if (pastEventsLink.classList.contains("is-active")) {
    return;
  }

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

  events.push(event);
  renderSortedEvents(events);
};

export {
  addEvent,
  renderSortedEvents,
  addEventAndRender,
  emptyContainer,
  findEventIndex,
  deleteEventAtIndex,
  getEvent,
};

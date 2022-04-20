import agent from "./../agent.js";
import { addEventAndRender } from "./../../events/event_container.js";
import {
  closeAddEventModal,
  clearEventInput,
} from "./../../events/event_add.js";

const postEvent = (event) => {
  console.log(event);
  const body = {
    name: event.name,
    occurrence: event.occurence.toPost(),
    description: event.description,
    addressZip: event.address.zip,
    address: {
      line1: event.address.line1,
      line2: event.address.line2,
      city: event.address.city,
      state: event.address.state,
      zip: event.address.zip,
      country: event.address.country,
    },
  };
  agent.events
    .create(body)
    .then(function (response) {
      console.log("1");
      addEventAndRender(response);
      closeAddEventModal();
      clearEventInput();
      return response;
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { postEvent };

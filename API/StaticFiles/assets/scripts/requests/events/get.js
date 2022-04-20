import { loadEvents } from "../../events/event_list.js";
import agent from "./../agent.js";

const loadPastEvents = () => {
  agent.events
    .listPast()
    .then((response) => {
      loadEvents(response);
      return response;
    })
    .catch(function (error) {
      console.log(error);
    });
};

const loadFutureEvents = () => {
  agent.events
    .listFuture()
    .then((response) => {
      loadEvents(response);
      return response;
    })
    .catch(function (error) {
      console.log(error);
    });
};

export { loadPastEvents, loadFutureEvents };

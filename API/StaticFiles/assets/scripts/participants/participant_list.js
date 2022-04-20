import { Person, Company } from "../model.js";
import {
  emptyParticipants,
  addParticipant,
  renderSortedParticipants,
} from "./participant_container.js";

const eventTitle = document.getElementById("event-title");

const loadParticipants = (data, condition) => {
  console.log(data);
  eventTitle.innerHTML = data.eventName;
  emptyParticipants();
  let participant;
  for (const obj of data.participants) {
    if (condition === "person") {
      participant = new Person(
        obj.eventId,
        obj.firstName,
        obj.lastName,
        obj.code,
        obj.paymentMethod,
        obj.description
      );
    } else if (condition == "company") {
      participant = new Company(
        obj.eventId,
        obj.name,
        obj.code,
        obj.participantCount,
        obj.paymentMethod,
        obj.description
      );
    }

    addParticipant(participant, condition);
  }

  renderSortedParticipants(condition);
};

export { loadParticipants };

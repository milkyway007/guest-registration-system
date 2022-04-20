import {
  loadEventPersons,
  loadEventCompanies,
} from "./requests/participants/get.js";
import { backdropClickHandler } from "./backdrop.js";
import { fillPaymentMethodSelect } from "./participants/participant_details_modal.js";

var eventPersonLinkContainer = document.getElementById(
  "event-person-link-container"
);
var eventCompanyLinkContainer = document.getElementById(
  "event-company-link-container"
);
var eventPersonLink = document.getElementById("event-person-link");
var eventCompanyLink = document.getElementById("event-company-link");
const backdrops = document.querySelectorAll(".backdrop");

const swapParticipantType = () => {
  eventPersonLinkContainer.classList.toggle("is-active");
  eventCompanyLinkContainer.classList.toggle("is-active");
};

const showEventPersons = () => {
  swapParticipantType();

  var eventId = getQueryParamByKey("event_id");

  loadEventPersons(eventId);
};

const showEventCompanies = () => {
  swapParticipantType();

  var eventId = getQueryParamByKey("event_id");

  loadEventCompanies(eventId);
};

document.addEventListener("DOMContentLoaded", (event) => {
  var eventId = getQueryParamByKey("event_id");

  loadEventPersons(eventId);
  fillPaymentMethodSelect();
});

eventPersonLink.addEventListener("click", showEventPersons);
eventCompanyLink.addEventListener("click", showEventCompanies);

for (const backdrop of backdrops) {
  backdrop.addEventListener("click", backdropClickHandler);
}

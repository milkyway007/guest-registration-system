import { loadFutureEvents, loadPastEvents } from "./requests/events/get.js";
import {
    showEventModalHandler,
    closeAddEventModal,
    addEventHandler,
    fillCountrySelectList,
    clearEventValidation,
} from "./events/event_add.js";
import { backdropClickHandler } from "./backdrop.js";
import { fillPaymentMethodSelect } from "./participants/participant_details_modal.js";

const futureEventsLink = document.getElementById("future-events-link");
const pastEventsLink = document.getElementById("past-events-link");
const startAddEventButton = document.getElementById("start-add-event-button");
const cancelAddEventButton = document.getElementById("cancel-add-event-button");
const addEventButton = document.getElementById("add-event-button");
const backdrops = document.querySelectorAll(".backdrop");
const eventDateTimeInput = document.getElementById("event-datetime-input");
const eventNameInput = document.getElementById("event-name-input");
const addressLine1Input = document.getElementById("address-line-1-input");
const addressCityInput = document.getElementById("address-city-input");
const addressCountrySelect = document.getElementById("address-country-select");
const addressZipInput = document.getElementById("address-zip-input");

const showFutureEvents = () => {
    loadFutureEvents();
};

const showPastEvents = () => {
    loadPastEvents();
};

document.addEventListener("DOMContentLoaded", (event) => {
    loadFutureEvents();
    fillCountrySelectList();
    fillPaymentMethodSelect();
});
futureEventsLink.addEventListener("click", showFutureEvents);
pastEventsLink.addEventListener("click", showPastEvents);
startAddEventButton.addEventListener("click", showEventModalHandler);
cancelAddEventButton.addEventListener("click", closeAddEventModal);
addEventButton.addEventListener("click", addEventHandler);

eventNameInput.addEventListener("input", clearEventValidation);
eventDateTimeInput.addEventListener("input", clearEventValidation);
addressLine1Input.addEventListener("input", clearEventValidation);
addressCityInput.addEventListener("input", clearEventValidation);
addressCountrySelect.addEventListener("input", clearEventValidation);
addressZipInput.addEventListener("input", clearEventValidation);

for (const backdrop of backdrops) {
    backdrop.addEventListener("click", backdropClickHandler);
}

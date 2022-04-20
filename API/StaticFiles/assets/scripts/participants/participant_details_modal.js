import { Person, Company } from "../model.js";
import { putPerson, putCompany } from "./../requests/participants/put.js";

const personPaymentMethodSelectContainer = document.querySelector(
  ".person-payment-method-select-container"
);
const personPaymentMethodSelect = document.querySelector(
  ".person-payment-method-select"
);
const companyPaymentMethodSelectContainer = document.querySelector(
  ".company-payment-method-select-container"
);
const companyPaymentMethodSelect = document.querySelector(
  ".company-payment-method-select"
);
const personFirstNameInput = document.querySelector(".person-firstname-input");
const personLastNameInput = document.querySelector(".person-lastname-input");
const personCodeInput = document.querySelector(".person-code-input");
const personDescription = document.querySelector(
  ".person-description-textarea"
);
const companyNameInput = document.querySelector(".company-name-input");
const companyCodeInput = document.querySelector(".company-code-input");
const companyParticipantCountInput = document.querySelector(
  ".company-participant-count-input"
);
const companyDescription = document.querySelector(
  ".company-description-textarea"
);

const personFirstNameValidation = document.querySelector(
  ".person-firstname-validation"
);
const personLastNameValidation = document.querySelector(
  ".person-lastname-validation"
);
const personCodeValidation = document.querySelector(".person-code-validation");
const personPaymentMethodValidation = document.querySelector(
  ".person-payment-method-validation"
);
const companyNameValidation = document.querySelector(
  ".company-name-validation"
);
const companyCodeValidation = document.querySelector(
  ".company-code-validation"
);
const companyPaymentMethodValidation = document.querySelector(
  ".company-payment-method-validation"
);
const companyParticipantCountValidation = document.querySelector(
  ".company-participant-count-validation"
);
const participantDetailsModal = document.querySelector(
  ".participant-details-modal"
);
var personLinkContainer = document.querySelector(".person-link-container");
var companyLinkContainer = document.querySelector(".company-link-container");
var personLink = document.querySelector(".person-link");
var companyLink = document.querySelector(".company-link");
var personForm = document.querySelector(".person-form");
var companyForm = document.querySelector(".company-form");

const paymentMethods = ["Pangaülekanne", "Sularaha"];

const showProperForm = (condition) => {
  if (condition == "person") {
    personForm.classList.remove("is-hidden");
    companyForm.classList.add("is-hidden");
  }

  if (condition == "company") {
    personForm.classList.add("is-hidden");
    companyForm.classList.remove("is-hidden");
  }
};

const showParticipantDetailsModal = () => {
  participantDetailsModal.classList.add("is-active");
};

const update = (participant, condition) => {
  if (condition === "person") {
    participant.firstName = personFirstNameInput.value;
    participant.lastName = personLastNameInput.value;
    participant.code = personCodeInput.value;
    participant.paymentMethod =
      personPaymentMethodSelect.value == paymentMethods[0] ? 1 : 2;
    participant.description = personDescription.value;

    putPerson(participant);

    return;
  }

  participant.name = companyNameInput.value;
  participant.code = companyCodeInput.value;
  participant.participantCount = companyParticipantCountInput.value;
  participant.paymentMethod =
    companyPaymentMethodSelect.value == paymentMethods[0] ? 1 : 2;
  participant.description = companyDescription.value;

  putCompany(participant);
};

const enableParticipantInput = () => {
  personFirstNameInput.disabled = false;
  personLastNameInput.disabled = false;
  personPaymentMethodSelect.disabled = false;
  personDescription.disabled = false;
  companyNameInput.disabled = false;
  companyParticipantCountInput.disabled = false;
  companyPaymentMethodSelect.disabled = false;
  companyDescription.disabled = false;
};

const initializeFields = (participant, condition) => {
  console.log(participant);
  console.log(participant.paymentMethod);
  if (condition === "person") {
    personFirstNameInput.value = participant.firstName;
    personLastNameInput.value = participant.lastName;
    personCodeInput.value = participant.code;
    personPaymentMethodSelect.value =
      participant.paymentMethod == 1 ? paymentMethods[0] : paymentMethods[1];
    personDescription.value = participant.description;

    return;
  }

  companyNameInput.value = participant.name;
  companyCodeInput.value = participant.code;
  companyParticipantCountInput.value = participant.participantCount;
  companyPaymentMethodSelect.value =
    participant.paymentMethod == 1 ? paymentMethods[0] : paymentMethods[1];
  companyDescription.value = participant.description;
};

const disableParticipantInput = () => {
  personFirstNameInput.disabled = true;
  personLastNameInput.disabled = true;
  personCodeInput.disabled = true;
  personPaymentMethodSelect.disabled = true;
  personDescription.disabled = true;
  companyNameInput.disabled = true;
  companyCodeInput.disabled = true;
  companyParticipantCountInput.disabled = true;
  companyPaymentMethodSelect.disabled = true;
  companyDescription.disabled = true;
};

const isPersonActive = () => {
  return personLinkContainer.classList.contains("is-active");
};

const createCompany = () => {
  const name = companyNameInput.value;
  const code = companyCodeInput.value;
  const paymentMethod = companyPaymentMethodSelect.value;
  const participantCount = companyParticipantCountInput.value;
  console.log(participantCount);
  const description = companyDescription.value;

  return new Company(
    0,
    name,
    code,
    participantCount,
    paymentMethod === paymentMethods[0] ? 1 : 2,
    description
  );
};

const createPerson = () => {
  const firstName = personFirstNameInput.value;
  const lastName = personLastNameInput.value;
  const code = personCodeInput.value;
  const paymentMethod = personPaymentMethodSelect.value;
  const description = personDescription.value;

  return new Person(
    0,
    firstName,
    lastName,
    code,
    paymentMethod === paymentMethods[0] ? 1 : 2,
    description
  );
};

const isPersonFirstNameValid = () => {
  if (!personFirstNameInput.value) {
    personFirstNameInput.classList.add("is-danger");
    personFirstNameValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isPersonLastNameValid = () => {
  if (!personLastNameInput.value) {
    personLastNameInput.classList.add("is-danger");
    personLastNameValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isPersonCodeValid = () => {
  if (!personCodeInput.value) {
    personCodeInput.classList.add("is-danger");
    personCodeValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isCompanyNameValid = () => {
  if (!companyNameInput.value) {
    companyNameInput.classList.add("is-danger");
    companyNameValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isCompanyCodeValid = () => {
  if (!companyCodeInput.value) {
    companyCodeInput.classList.add("is-danger");
    companyCodeValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isPersonPaymentMethodValid = () => {
  if (!paymentMethods.includes(personPaymentMethodSelect.value)) {
    personPaymentMethodSelectContainer.classList.add("is-danger");
    personPaymentMethodValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isCompanyPaymentMethodValid = () => {
  if (!paymentMethods.includes(companyPaymentMethodSelect.value)) {
    companyPaymentMethodSelectContainer.classList.add("is-danger");
    companyPaymentMethodSelect.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isCompanyParticipantCountValid = () => {
  if (companyParticipantCountInput.value < 1) {
    companyParticipantCountInput.classList.add("is-danger");
    companyParticipantCountValidation.classList.remove("is-hidden");

    return false;
  }

  return true;
};

const isPersonFormValid = () => {
  const isFirstNameValid = isPersonFirstNameValid();
  const isLastNameValid = isPersonLastNameValid();
  const isCodeValid = isPersonCodeValid();
  const isPaymentMethodValid = isPersonPaymentMethodValid();

  return (
    isFirstNameValid && isLastNameValid && isCodeValid && isPaymentMethodValid
  );
};

const isCompanyFormValid = () => {
  const isNameValid = isCompanyNameValid();
  const isCodeValid = isCompanyCodeValid();
  const isPaymentMethodValid = isCompanyPaymentMethodValid();
  const isParticipantCountValid = isCompanyParticipantCountValid();

  return (
    isNameValid &&
    isCodeValid &&
    isPaymentMethodValid &&
    isParticipantCountValid
  );
};

const clearParticipantValidation = () => {
  personFirstNameInput.classList.remove("is-danger");
  personFirstNameValidation.classList.add("is-hidden");
  personLastNameInput.classList.remove("is-danger");
  personLastNameValidation.classList.add("is-hidden");
  personCodeInput.classList.remove("is-danger");
  personCodeValidation.classList.add("is-hidden");
  personPaymentMethodSelectContainer.classList.remove("is-danger");
  personPaymentMethodValidation.classList.add("is-hidden");
  companyNameInput.classList.remove("is-danger");
  companyNameValidation.classList.add("is-hidden");
  companyCodeInput.classList.remove("is-danger");
  companyCodeValidation.classList.add("is-hidden");
  companyParticipantCountInput.classList.remove("is-danger");
  companyParticipantCountValidation.classList.add("is-hidden");
  companyPaymentMethodSelectContainer.classList.remove("is-danger");
  companyPaymentMethodValidation.classList.add("is-hidden");
};

const clearParticipantInput = () => {
  personFirstNameInput.value = "";
  personLastNameInput.value = "";
  personCodeInput.value = "";
  personPaymentMethodSelectContainer.value = "";
  personPaymentMethodSelect.value = "";
  personDescription.value = "";
  companyNameInput.value = "";
  companyCodeInput.value = "";
  companyParticipantCountInput.value = "";
  companyPaymentMethodSelectContainer.value = "";
  companyPaymentMethodSelect.value = "";
  companyDescription.value = "";
};

const swapForm = () => {
  personLinkContainer.classList.toggle("is-active");
  companyLinkContainer.classList.toggle("is-active");

  personForm.classList.toggle("is-hidden");
  companyForm.classList.toggle("is-hidden");
};

function closeParticipantDetailsModal() {
  if (!participantDetailsModal) {
    return;
  }
  participantDetailsModal.classList.remove("is-active");
  clearParticipantInput();
  clearParticipantValidation();
  if (!isPersonActive()) {
    swapForm();
  }
}

const fillPaymentMethodSelect = () => {
  console.log("fillPaymentMethodSelect");
  paymentMethods.sort();
  console.log(paymentMethods);
  for (var i = 0; i < paymentMethods.length; i++) {
    var option = document.createElement("option");
    option.value = paymentMethods[i];
    option.text = paymentMethods[i];
    console.dir(option);
    personPaymentMethodSelect.appendChild(option);
    companyPaymentMethodSelect.appendChild(option.cloneNode(true));
  }

  personPaymentMethodSelect.value = "Vali";
  companyPaymentMethodSelect.value = "Vali";
};

personFirstNameInput.addEventListener("input", clearParticipantValidation);
personLastNameInput.addEventListener("input", clearParticipantValidation);
personCodeInput.addEventListener("input", clearParticipantValidation);
personPaymentMethodSelect.addEventListener("input", clearParticipantValidation);

companyNameInput.addEventListener("input", clearParticipantValidation);
companyCodeInput.addEventListener("input", clearParticipantValidation);
companyParticipantCountInput.addEventListener(
  "input",
  clearParticipantValidation
);
companyPaymentMethodSelect.addEventListener(
  "input",
  clearParticipantValidation
);

personLink.addEventListener("click", swapForm);
companyLink.addEventListener("click", swapForm);

export {
  fillPaymentMethodSelect,
  isPersonFormValid,
  isCompanyFormValid,
  createPerson,
  createCompany,
  closeParticipantDetailsModal,
  swapForm,
  personLinkContainer,
  companyLinkContainer,
  personLink,
  companyLink,
  personForm,
  companyForm,
  isPersonActive,
  initializeFields,
  disableParticipantInput,
  enableParticipantInput,
  update,
  showParticipantDetailsModal,
  showProperForm,
  clearParticipantValidation,
};

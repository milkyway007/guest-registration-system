import { isAnyBackdropActive } from "./backdrop.js";

const message = document.querySelector(".warning");

const showServerError = (error) => {
  console.log(error);
  const setText = () => {
    const body = message.querySelector(".message-body");

    const errorMessage = document.createElement("p");
    const details = message.cloneNode(true);
    errorMessage.innerText = error.message;
    details.innerText = error.details;

    body.appendChild(errorMessage);
    body.appendChild(details);
  };

  showWarning(setText);
};

const showValidationError = (array) => {
  const setText = () => {
    const body = message.querySelector(".message-body");

    const ul = document.createElement("ul");
    for (const element in array) {
      const li = document.createElement("li");
      li.innerText = element;
      ul.appendChild(li);
    }
    body.appendChild(ul);
  };

  showWarning(setText);
};

const hideMessage = () => {
  const backdrop = message.querySelector(".backdrop");
  backdrop.classList.remove("transparent");

  const buttons = document.querySelectorAll(".button");
  for (const button of buttons) {
    button.disabled = false;
  }

  const body = message.querySelector(".message-body");
  removeChildren(body);

  message.classList.remove("is-active");
};

const showMessage = (text) => {
  const setText = () => {
    const body = message.querySelector(".message-body");
    body.innerText = text;
  };

  showWarning(setText);
};

const showWarning = (func) => {
  const backdrop = message.querySelector(".backdrop");
  if (isAnyBackdropActive()) {
    backdrop.classList.add("transparent");
  }

  func();

  const buttons = document.querySelectorAll(".button");
  for (const button of buttons) {
    button.disabled = true;
  }

  const button = message.querySelector(".delete");
  button.disabled = false;

  button.removeEventListener("click", hideMessage);
  button.addEventListener("click", hideMessage);

  message.classList.add("is-active");
};

export { showMessage, hideMessage, showValidationError, showServerError };

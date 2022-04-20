function padTo2Digits(num) {
  const result = num.toString().padStart(2, "0");

  return result;
}

const removeChildren = (parent) => {
  while (parent.lastElementChild) {
    const child = parent.lastElementChild;
    parent.removeChild(child);
  }
};

const getQueryParamByKey = (key) => {
  const handler = { get: (searchParams, prop) => searchParams.get(prop) };
  const proxy = new Proxy(new URLSearchParams(window.location.search), handler);

  return proxy[key];
};

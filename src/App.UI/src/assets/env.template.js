

(function(window) {
  window._env = window._env || {};

  // Environment variables
  window._env.apiUrl = "${API_URL}" || '/';
  window._env.debug = "${DEBUG}" === "true" || false;
})(this);
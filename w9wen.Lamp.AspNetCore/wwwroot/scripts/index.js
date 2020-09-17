var matchedCurrency = {
  en: "USD",
  de: "EUR",
  ar: "AED",
  zh: "TWN",
  "fr-CH": "CHF",
};

function changeCulture(cul) {
  ej.base.setCurrencyCode(
    sessionStorage.getItem("ej2-currency") || matchedCurrency[cul]
  );
  ej.base.setCulture(cul);
}

function loadCulture() {
  var cul = sessionStorage.getItem("ej2-culture") || "en";

  if (cul !== "en") {
    var locale = new ej.base.Ajax(
      "../scripts/locale/" + cul + ".json",
      "GET",
      false
    );
    locale.send().then(function (value) {
      ej.base.L10n.load(JSON.parse(value));
    });

    var ajax = new ej.base.Ajax(
      "../scripts/cldr-data/main/" + cul + "/all.json",
      "GET",
      false
    );

    ajax.send().then(function (result) {
      ej.base.loadCldr(JSON.parse(result));
      changeCulture(cul);
    });
  }
}

function loadJSON() {
  sessionStorage.setItem("ej2-culture", "zh");
  sessionStorage.setItem("ej2-currency", "zh");
  loadCulture();
}

loadJSON();

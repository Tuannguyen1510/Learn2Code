const renderLang = document.getElementsByClassName("lang-grid")[0];
const topLang = document.getElementsByClassName("langtag")[0];
const dataLang = [
  { name: "HTML", backgroundColor: "--green-w3", color: "white" },
  { name: "CSS", backgroundColor: "--light-pink-w3", color: "black" },
  { name: "JavaScript", backgroundColor: "--yellow-w3", color: "black" },
  { name: "SQL", backgroundColor: "--light-pink-w3", color: "black" },
  { name: "C++", backgroundColor: "--light-black-w3", color: "white" },
  { name: "Python", backgroundColor: "--light-blue-w3", color: "white" },
];

const dataTopLang = [
  {
    name: "HTML",
    title: "Ngôn ngữ xây dựng trang web",
    backgroundColor: "background-color: var(--light-blue-w3)",
    color: "color: white !important;",
    filePath: "html.jpg",
  },
  {
    name: "CSS",
    title: "Ngôn ngữ giúp trang trí cho trang web của bạn",
    backgroundColor: "background-color: var(--light-pink-w3)",
    color: "color: black;",
    filePath: "css.jpg",
  },
  {
    name: "JavaScript",
    title: "Ngôn ngữ dành cho lập trình web",
    backgroundColor: "background-color: var(--light-black-w3)",
    color: "color: white !important;",
    filePath: "js.jpg",
  },
  {
    name: "Python",
    title: "Ngôn ngữ lập trình thông dụng nhất thế giới",
    backgroundColor: "background-color: var(--yellow-w3)",
    color: "color: black;",
    filePath: "python.jpg",
  },
];

const renderDataLang = dataLang
  .map(
    (item) =>
      `<div style="background-color: var(${item.backgroundColor}); color:${item.color}">${item.name}</div>`
  )
  .join("");

console.log(renderDataLang);

const renderDataTopLangHTML = dataTopLang
  .map(
    (item) => `
       <div class="langtag-component" style="${item.backgroundColor};${item.color}">
         <div class="component-boxing">
          <div class="boxing-lang box-margin">
            <h2>${item.name}</h2>
            <p>${item.title}</p>
            <button >Học ${item.name}</button>
          </div>
          <div class="boxing-example box-margin">
            <p>${item.name} Example</p>
            <img src="~/layout_home/img/lang/${item.filePath}"/>
          </div>
         </div>
      </div>
`
  )
  .join("");

topLang.innerHTML = renderDataTopLangHTML;

renderLang.innerHTML = renderDataLang;

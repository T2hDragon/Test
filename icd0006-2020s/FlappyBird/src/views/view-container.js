export default function viewContainer() {
    let content = document.createElement('div');
    content.classList.add("view-container");

    content.innerText = '';

    return content;
}
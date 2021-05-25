export default function viewContainer(): HTMLDivElement {
    let content: HTMLDivElement = document.createElement('div');
    content.classList.add("view-container");

    content.innerText = '';

    return content;
}
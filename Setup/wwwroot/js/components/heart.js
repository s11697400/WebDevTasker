class Heart extends HTMLElement {
    shadowRoot;
    templateId = 'heart-tpl';
    elementId = 'heart';

    amountHearts = 3;
    constructor() {
        super(); // always call super() first in the ctor.
        this.shadowRoot = this.attachShadow({mode: 'open'});
        this.state = {
            heartclicks: 0
        };
        this.attachStyling();
        this.attachContent();
    }
    connectedCallback() {
        console.log('heart: connected to DOM');
    }

    disconnectedCallback(){
        console.log('heart: disconnected from DOM');
    }

    attachContent(){
        const self = this.shadowRoot;
        for (let index = 0; index < this.amountHearts; index++) {            
            let heart = document.createElement("img");
            heart.setAttribute("src", "/Images/heart.png");
            heart.setAttribute("width", "50");
            heart.setAttribute("height", "50");
            self.appendChild(heart);
        }
        
    }

    attachStyling(){
        const linkElem = document.createElement("link");
        linkElem.setAttribute("rel", "stylesheet");
        linkElem.setAttribute("href", "/css/heart.css");
        this.shadowRoot.appendChild(linkElem);
    }

}

export {Heart};

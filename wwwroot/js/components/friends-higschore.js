export default class FriendsHighscore extends HTMLElement {
        shadowRoot;
    templateId = 'friends-item-tpl';
    elementId = 'friends-item';
    friend;
	constructor(friend) {
        console.log(friend);
        console.log("hoi"); 
        super(); // always call super() first in the ctor.
        this.shadowRoot = this.attachShadow({mode: 'open'});
          this.state = {
            friend: friend,
        };
        this.friend = friend;
    this.applyTemplate();
        this.applyEventlisteners();
        this.setState('friend', friend);
    }
     applyTemplate() {
        let template = document.getElementById(this.templateId);
        let clone = template.content.cloneNode(true);

        this.shadowRoot.appendChild(clone);
      
    }
    connectedCallback() {
        console.log('item: connected to DOM');
        console.log(this.shadowRoot);
        this.shadowRoot.querySelector('td[data-bind="name"]').textContent =  this.friend["user2"]["userName"];
         this.shadowRoot.querySelector('td[data-bind="score"]').textContent =  this.friend["user2"]["highScore"];
    }

    disconnectedCallback() {
        console.log('item: disconnected from DOM');

    }
     applyEventlisteners() {
        // console.log('Click event in boodschappen-item.js nog niet ingeschakeld.');
        const self = this;
        this.addEventListener('click', () => {
               console.log(self);
        });
         
    }
    setState(key, value) {
        this.state[key] = value;
        this.updateBinding(key);
    }
    
    updateBinding(prop) {
        let bindings = this.shadowRoot.querySelectorAll(`[data-bind$="${prop}"]`);
        // https://developer.mozilla.org/en-US/docs/Web/API/HTMLElement/dataset
        bindings.forEach(node => {
            node.textContent = this.state[prop];
        })

    }
}
	customElements.define('friends-highscore', FriendsHighscore);

export {FriendsHighscore};

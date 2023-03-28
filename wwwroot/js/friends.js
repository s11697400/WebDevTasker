import FriendsHighscore from './components/friends-higschore.js';

class FriendsApp extends HTMLElement {

    shadowRoot;
    templateId = 'friends-app-tpl';
    elementId = 'Friends-app';
    friendList = {};
    user;
    constructor() {
        super();
        this.user = this.getAttribute("user");
        console.log(this.user);
        this.shadowRoot = this.attachShadow({ mode: 'open' })
        this.getFriends();

        this.connectedCallback();
    }

    connectedCallback() {
        this.applyTemplate();
        this.attachStyling();
        


        //student uitwerking

    }
    getFriends() {
        //fetch...
        if (Object.keys(this.friendList).length === 0) {
            console.log("if passed");
            const data = fetch("/api/Friendships/" + this.user).then((response) => response.json())
                .then((data) => this.SetFriends(data));
        } else {
            console.log("else passed");
            this.SetFriends(this.friendList);
        }
        //student uitwerking
    }
    SetFriends(data) {
       
        this.friendList = data;
        console.log(data);
        console.log(this.friendList);
        const item = new FriendsHighscore(this.friendList);
        this.shadowRoot.querySelector("tbody").appendChild(item);
    }
    applyTemplate() {
        let appTemplate = document.getElementById(this.templateId);
      let clone = appTemplate.content.cloneNode(true);
        this.shadowRoot.appendChild(clone);
       
        //clone template an voeg toe aan shadowRoot
        //student uitwerking

    }

    // app.js
    attachStyling() {
        const linkElem = document.createElement("link");

        this.shadowRoot.appendChild(linkElem);
    }

}

customElements.define('friends-app', FriendsApp);

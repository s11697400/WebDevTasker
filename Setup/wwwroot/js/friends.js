import FriendsHighscore from './components/friends-higschore.js';
import FriendsRequests from './components/friends-requests.js';
import FriendsSearch from './components/friends-search.js';
export default class FriendsApp extends HTMLElement {

    shadowRoot;
    templateId = 'friends-app-tpl';
    elementId = 'Friends-app';
    friendList = {};
    user;
    requestList = {};
    constructor() {
        super();
        this.user = this.getAttribute("user");
        console.log(this.user);
        this.shadowRoot = this.attachShadow({ mode: 'open' })

        this.getFriends();

    }
    
    connectedCallback() {
        this.applyTemplate();
        this.attachStyling();
        

               
        //student uitwerking

    }
    SetFriends(data) {
        console.log(data);
           let arrayAccepted = [];
           let arrayRequest= [];
        data.forEach(element => {
            if(element["accepted"]){
                arrayAccepted.push(element);
            }
            else{
                arrayRequest.push(element);
            }
        });
    
    
        this.friendList = arrayAccepted;
        this.friendList.forEach(element => {
                const item = new FriendsHighscore(element);
            this.shadowRoot.querySelector(".table-friends--body").appendChild(item);
            });
            this.requestList = arrayRequest;
            console.log(this.requestList);
            this.requestList.forEach(element => {
                const item = new FriendsRequests(element);
                this.shadowRoot.querySelector(".table-requests--body").appendChild(item);
            })
        }
    getFriends() {
        //fetch...
        if (Object.keys(this.friendList).length === 0) {
            const data = fetch("/api/Friendships/" + this.user).then((response) => response.json())
                .then((data) => this.SetFriends(data));
        } else {
            this.SetFriends(this.friendList);
        }
        //student uitwerking
    }
 
    applyTemplate() {
        let appTemplate = document.getElementById(this.templateId);
      let clone = appTemplate.content.cloneNode(true);
        this.shadowRoot.appendChild(clone);
        const item = new FriendsSearch(this.user);
        this.shadowRoot.querySelector(".search-friends--body").appendChild(item);
    }

    // app.js
    attachStyling() {
        const linkElem = document.createElement("link");
        linkElem.setAttribute("rel", "stylesheet");
        linkElem.setAttribute("href", "/css/friends.css");

        this.shadowRoot.appendChild(linkElem);
    }

}

customElements.define('friends-app', FriendsApp);

export {FriendsApp};

import { CHANGE_SERVER } from "../actions/serverActions";
import { NodeJsURL } from "../../apiClient/serverURLs";

export default function serverReducer(server, { type, url }) {

    switch (type) {

        case CHANGE_SERVER:
            return url;

        default:
            return server ? server : NodeJsURL;
    }
}

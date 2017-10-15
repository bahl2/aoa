var twit = require('twit');
var config = require('./config.js');
var Twitter = new twit(config);
var hashtag = "thisisahashtag"; // hashtag that we are looking for
var max_id = '';
function twitterSearch() {
	twitter.search({
		q: "#" + hashtag,
		result_type: "recent",
		max_id: max_id,
		count: 100 // MAX RETURNED ITEMS IS 100
	},
		session.accessToken,
		session.accessTokenSecret,
		function (error, data, response) {
			for (var i = 0; i < data.statuses.length; i++) {
				var id = data.statuses[i].id;
				if (max_id == '' || id < parseInt(max_id)) {
					max_id = id;
				}
			}

			// GET MORE TWEETS
			if (max_id != '0')
				twitterSearch();
		}
	);
}
}

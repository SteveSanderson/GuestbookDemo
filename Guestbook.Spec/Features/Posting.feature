Feature: Posting
	In order to express my views
	As a user
	I want to be able to post entries into the guestbook

Scenario: Navigation to posting page
	Given I am on the guestbook page
	Then I should see a button labelled "Post a New Entry"
	When I click the button labelled "Post a New Entry"
	Then I am on the posting page

Scenario: Viewing the posting page
    Given I am on the posting page
	Then I should see a field labelled "Your name"
	 And I should see a field labelled "Your comment"

Scenario: Posting a valid entry
	Given I am on the posting page
	  And I have filled out the form as follows
	    | Label          | Value             |
	    | Your name      | Jakob             |
		| Your comment   | Das ist gut!      |
    When I click the button labelled "Post"
	Then I should be on the guestbook page
	 And I see the flash message "Thanks for posting!"
	 And the guestbook entries includes the following
	    | Name      | Comment      | Posted date          |
		| Jakob     | Das ist gut! | (within last minute) |




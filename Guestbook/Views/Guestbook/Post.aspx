<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Guestbook.Domain.Entities.GuestbookEntry>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Guestbook : Post a New Entry
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Post</h2>

    <% using(Html.BeginForm()) { %>
       <div>
            <label for="Author">Your name:</label>
            <%= Html.EditorFor(x => x.Author) %>
       </div>
       <div>
            <label for="Comment">Your comment:</label>
            <%= Html.EditorFor(x => x.Comment) %>
       </div>
       <button type="submit">Post</button>
    <% } %>

</asp:Content>
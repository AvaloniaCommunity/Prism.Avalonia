# Contributing

Prism.Avalonia is an open-source project under the MIT license. We encourage community members like yourself to contribute.

You can contribute today by creating a **feature request**, **issue**, or **discussion** on the forum. From there we can have a brief discussion as to where this fits into the backlog priority. If this is something that fits within the Prism architecture, we'll kindly ask you to create a **Pull Request**. Any PR made without first having an issue/discussion may be closed.

Issues posted without a description may be closed immediately. Use the discussion boards if you have a question, not Issues.

We will close your _PR_ if it doesn't have an approved issue / feature request.

We reserve the right to close your "_issue_" or feature request if:

* It's an inquiry, not an issue.
* Error in your code for not following the documentation
* Not providing a description and steps to reproduce
* Not providing a sample when asked to do so

## "Keep It Prism"

"_Keep it Prism_" simply means:

* Does it work this way for Prism.WPF?
  * _or, Prism.Uno, Prism.Maui, etc?_

There have been requests in the past where individuals wanted changes for the sake of personal ease rather than how it would affect the ecosystem compatibility. This project should maintain compatibility with other Prism components, not just _one-offs_.

## Branching Strategy

Below is a basic branching hierarchy and strategy.

| Branch | Purpose
|-|-|
| `master`    | All releases are tagged published using the `master` branch
| `develop`   | The **default** & active development branch. When a feature set is completed and ready for public release, the `develop` branch will be merged into `master` and a new NuGet package will be published.
| `feature/*` | New feature branch. Once completed, it is merged into `develop` and the branch must be deleted.
| `stable/*`  | Stable release base build which shares cherry-picked merges from `develop`. This branch **must not** be deleted.

## Regards

> Thank you,
>
> Damian Suess<br />
> Xeno Innovations, Inc. / Suess Labs
